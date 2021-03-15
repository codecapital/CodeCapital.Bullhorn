using CodeCapital.Bullhorn.Api;
using CodeCapital.Bullhorn.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bullhorn.CommandLine.Services
{
    public class PlayGroundService
    {
        private readonly ILogger<PlayGroundService> _logger;
        private readonly BullhornApi _bullhornApi;

        public PlayGroundService(ILogger<PlayGroundService> logger, BullhornApi bullhornApi)
        {
            _logger = logger;
            _bullhornApi = bullhornApi;
        }

        public async Task TestApiAsync()
        {
            try
            {
                await _bullhornApi.CheckConnectionAsync();
                await GetDepartmentsAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "PlayGroundService");

                throw;
            }
        }

        public void TestJson()
        {
            var people = new List<Person>
            {
                new Person("Vaso", 20, "Bethlehem House", true, null),
                new Person("John", 80, "Cable Street", false, "Email"),
                new Person("Andy", 24, null, false, "Email"),
                new Person(null, 15, "Lime House Street", true, null),
            };

            var helper = new JsonHelper();

            var result1 = helper.Flatten(JsonSerializer.Serialize(people), 2, true);
            var result2 = helper.Flatten(JsonSerializer.Serialize(new { Humans = people }), 2);
        }

        private async Task GetDepartmentsAsync()
        {
            var testUrl = "Department?fields=id,description,enabled,name&where=id>0";

            var result = await _bullhornApi.QueryAsync<DepartmentDto>(testUrl);

            _logger.LogInformation("Items: {0}", result.Count);
        }
    }

    public class JsonHelper
    {
        private readonly List<dynamic> _data = new();
        private readonly Stack<string> _context = new();
        private ExpandoObject? _expandoObject = null;
        private string _currentPath = null!;
        private int _nestingLevel = 0;
        private int _maxDepth = 0;
        private bool _removeIntended = false;

        /// <summary>
        /// The delimiter "." used to separate individual keys in a path.
        /// </summary>
        public static readonly string KeyDelimiter = ".";

        public List<dynamic> Flatten(string json, int maxDepth = 1, bool removeIntended = false)
        {
            if (maxDepth <= 0) throw new ArgumentException($"{maxDepth} must be more than 0");

            var jsonDocumentOptions = new JsonDocumentOptions
            {
                CommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            };

            _data.Clear();
            _context.Clear();
            _expandoObject = null;
            _currentPath = null!;
            _nestingLevel = 0;
            _maxDepth = maxDepth + 1; // because of root
            _removeIntended = removeIntended;

            using var doc = JsonDocument.Parse(json, jsonDocumentOptions);

            if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                VisitElement(doc.RootElement);
            }
            else if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                VisitValue(doc.RootElement);
            }
            else
                throw new FormatException($"Unsupported JSON token '{doc.RootElement.ValueKind}' was found.");

            return _data;
        }

        private void VisitElement(JsonElement element)
        {
            foreach (var property in element.EnumerateObject())
            {
                EnterContext(property.Name);
                VisitValue(property.Value);
                ExitContext();
            }
        }

        private void VisitValue(JsonElement value)
        {
            IncreaseNesting();

            switch (value.ValueKind)
            {
                case JsonValueKind.Object:

                    if (_nestingLevel > _maxDepth)
                        AddJsonValue(_currentPath, value);
                    else
                        VisitElement(value);
                    break;

                case JsonValueKind.Array:

                    foreach (var arrayElement in value.EnumerateArray())
                    {
                        _expandoObject = new ExpandoObject();

                        VisitValue(arrayElement);

                        _data.Add(_expandoObject);
                    }
                    break;

                case JsonValueKind.Number:
                case JsonValueKind.String:
                case JsonValueKind.True:
                case JsonValueKind.False:

                    AddJsonValue(_currentPath, value);

                    break;

                case JsonValueKind.Null:

                    AddNullValue(_currentPath);

                    break;

                default:
                    throw new FormatException($"Unsupported JSON token '{value.ValueKind}' was found.");
            }

            DecreaseNesting();
        }

        private void AddJsonValue(string key, JsonElement value)
        {
            if (_removeIntended && value.ValueKind is JsonValueKind.Object)
                AddKeyValue(key, RemoveIndentation(value));
            else
                AddKeyValue(key, value);

            void AddKeyValue(string keyItem, object valueItem)
                => (_expandoObject as IDictionary<string, object>)?.Add(keyItem, valueItem);

            static string RemoveIndentation(JsonElement valueItem)
                => valueItem.ToString()?.Replace("\t", "").Replace("\n", "") ?? string.Empty;
        }

        private void AddNullValue(string key)
            => (_expandoObject as IDictionary<string, object>)?.Add(key, "NULL");

        private void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = Combine(_context.Reverse());
        }

        private void ExitContext()
        {
            _context.Pop();
            _currentPath = Combine(_context.Reverse());
        }

        private static string Combine(IEnumerable<string> pathSegments)
        {
            if (pathSegments == null)
            {
                throw new ArgumentNullException(nameof(pathSegments));
            }
            return string.Join(KeyDelimiter, pathSegments);
        }

        private void IncreaseNesting() => _nestingLevel++;
        private void DecreaseNesting() => _nestingLevel--;
    }

    public class Person
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public Address? MyAddress { get; set; }

        public Person(string? name, int age, string? street, bool isLocal, string? deliveryType)
        {
            Name = name;
            Age = age;

            if (street != null) MyAddress = new Address(street, isLocal, deliveryType);
        }

        public class Address
        {
            public string? Street { get; set; }
            public bool IsLocal { get; set; }
            public Delivery Delivery { get; set; }

            public Address(string? street, bool isLocal, string? deliveryType)
            {
                Street = street;
                IsLocal = isLocal;
                Delivery = new Delivery(deliveryType);
            }
        }

        public class Delivery
        {
            public string Type { get; set; } = "Post";

            public Delivery(string? type)
            {
                if (type != null) Type = type;
            }
        }
    }
}
