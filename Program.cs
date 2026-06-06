using Newtonsoft.Json.Linq;

string json = @"
{
  ""name"": {
    ""first"": ""Robert"",
    ""middle"": """",
    ""last"": ""Smith""
  },
  ""age"": 25,
  ""DOB"": ""-"",
  ""hobbies"": [
    ""running"",
    ""coding"",
    ""-""
  ],
  ""education"": {
    ""highschool"": ""N/A"",
    ""college"": ""Yale""
  }
}";

string[] INVALID_VALUES = ["N/A", "-", ""];

async Task<JObject> RemoveInvalidValues(JObject jsonObject)
{
    JObject newObj = new JObject();

    foreach (var prop in jsonObject.Properties().ToList())
    {
        var value = prop.Value;

        if (value.Type == JTokenType.String)
        {
            var stringValue = value.ToString();

            if (stringValue != "N/A" &&
                stringValue != "-" &&
                stringValue != string.Empty)
            {
                newObj[prop.Name.ToString()] = stringValue;
            }
        }
        else if (value is JArray array)
        {
            newObj[prop.Name.ToString()] = new JArray(
                array
                    .Select(v => v.ToString().ToLower())
                    .Except(INVALID_VALUES)
            );
        }
        else if (value is JObject nestedObject)
        {
            newObj[prop.Name.ToString()] = await RemoveInvalidValues(nestedObject);
        }
        else
        {
            newObj[prop.Name.ToString()] = prop.Value;
        }
    }

    return newObj;
}

var jobject = JObject.Parse(json);

var newJObject = await RemoveInvalidValues(jobject);

Console.WriteLine(newJObject.ToString());