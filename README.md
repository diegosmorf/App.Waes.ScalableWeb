## App.Waes.ScalableWeb

#Assumptions:

1-) 
Branch Strategy: Gitflow
feature branch >> develop >> test >> master

2-)
Domain Driven Development
2.1- Domain : represent domain model
2.2- Application : expose domain behavior/features to external world
2.3- Infrastructure: implementation of code based on external items ( database / email and etc)
2.4- Tests: represents unittest and integration tests
2.5- EntryPoints: Web project exposing data through api. 

3-)
Tecnologies: Owin / Nlog / Autofac

4-) Save Left content ( InMemory repository)
API: <host>/v1/diff/<ID>/left
Method: POST
Parameter:
1 - Type: int / Name: id
2 - Type: string / Name: content  (base64 encoded)

5-) Save right content ( InMemory repository)
API: <host>/v1/diff/<ID>/right
Method: POST
Parameter:
1 - Type: int / Name: id
2 - Type: string / Name: content  (base64 encoded)

6-) Compare Left And Right content ( InMemory repository)
API: <host>/v1/diff/<ID>
Method: POST
>> apply comparison between binary data
>> return Difference Comparison Results

7-)Improvements:
- Binary comparison can be implement using Paralels
- Content comparison can consider data with different size/length
- Create new comparison strategy based on google diff match patch - https://code.google.com/p/google-diff-match-patch/
