# Couchbase and Elasticsearch testing
Just playing around with Couchbase and Elasticsearch for C#.NET. <br>
Bulkinsert 5 000 000 rows to each "db" and then trying a date range query with pagination.<br>


## Notes (2016-08-06)
Couchbase: <br>
When doing items.Count() on query when i use 'Linq2Couchbase' i get the following error: <br>
```
{"Error performing bulk get operation  - cause: {4 errors, starting with WSASend tcp 127.0.0.1:3831: An existing connection was forcibly closed by the remote host.}"}
```
<br>
When doing bulkinserts (bucket.Upsert(items)) with 10000 items a time some items don't get inserted? Probably need to check if successful and if not re-insert to Couchbase.<br>
In general Couchbase feels unfinished when it comes to the .NET libraries especially when querying.

Elasticsearch: <br>
Feels fast stable and the query language for .NET feels complete and covers most of the usage scenarios for me ATM.<br>


## Config
Couchbase Per Node RAM Quota for bucket: 3GB<br>


<br>
<br>
Tested on:<br>
Windows 10 Pro<br>
Intel Xeon CPU E3-1505M v5 @ 2.8GHz<br>
32 GB RAM<br>
Nvidia Quadro M10000M<br>



## Links
Couchbase:<br>
http://blog.couchbase.com/2016/may/couchbase-with-windows-.net-part-4-linq2couchbase <br>
https://github.com/couchbaselabs/Linq2Couchbase <br>
http://blog.couchbase.com/facet/Topic/.NET <br>


Elasticsearch: <br>
https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/date-range-query-usage.html <br>