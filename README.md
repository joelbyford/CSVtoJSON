# CSVtoJSON (.NET FRAMEWORK VERSION)
**IMPORTANT: For a .NET Core version of this repo, please see [joelbyford/CSVtoJSONcore](https://github.com/joelbyford/CSVtoJSONcore) instead.**

## From Original Fork:
Deploy the master branch for a universal CSV to JSON converter (returns an array of JSON Objects with properties named from the header row)

The `dev` branch has the sample CSV data shown in the //Build session [here](https://channel9.msdn.com/Events/Build/2016/P462) (CSV file included `dev` branch)

## Extension from Original Fork:
Optional querystring parameter to allow consumers to specify the delimiter being used in the text file and allows for other delimiters (such as | or ;).  Usage is:

mydomain.com/myservice/api/?delimiter=|

If no delimiter parameter is provided, comma is assumed.  Example test through Postman or REST plugin in VSCode:

```
POST https://SOME-WEBSITE-URL/api/csvtojson
Content-Type: text/csv

this,is,a,test
1,2,3,4
a,b,c,d
5,6,7,8
```
