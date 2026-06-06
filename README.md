# JSON Invalid Value Cleaner (.NET)

This project provides a utility method to recursively clean JSON objects by removing or filtering out invalid or unwanted values.

## 🚀 Purpose

The goal of this utility is to sanitize JSON data by removing:

- `"N/A"`
- `"-"`
- Empty strings (`""`)
- Optional invalid values in arrays
- Nested invalid values inside objects

## 🧠 How It Works

The method recursively traverses a `JObject` and processes:

- **Strings** → Removed if they match invalid values
- **Arrays** → Filtered using a predefined invalid values list
- **Objects** → Cleaned recursively
- **Other types** → Either preserved or ignored depending on implementation

# How to run

`dotnet run`