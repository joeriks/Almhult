##Basic idea

Just run the solution from command line and you get a rest interface on localhost:8080 for any json data.

The data stores as Json in Esent (the native windows data sln).

###Built in REST-interface

	/{resource} : gets all items
	/{resource}/{id} : gets one resource item
	/{resource} (data) [post] : creates new item
	/{resource}/{id} (data) [put] : updates item

####Sample

Shows all items for a specific resource:

	/foos 

	[{
	  "foo": "BC",
	  "_date": "2014-02-05T14:55:16.785635+01:00",
	  "_id": "1C8F1EA26F7D4225AD4825A536D8D0FD",
	  "_rev": 3
	},{
	  "foo": "AA",
	  "_date": "2014-02-05T14:48:07.3833434+01:00",
	  "_id": "E8AEC37C3B4A4E0F83A61894C0E495FA"
	}]

	The resource has two stored items, the "BC" one is the 3nd revision with that id.

Show individual item:

	/foos/1C8F1EA26F7D4225AD4825A536D8D0FD
	
	{
	  "foo": "BC",
	  "_date": "2014-02-05T14:55:16.785635+01:00",
	  "_id": "1C8F1EA26F7D4225AD4825A536D8D0FD",
	  "_rev": 3
	}

Show older revision for individual item:

	/foos/1C8F1EA26F7D4225AD4825A536D8D0FD?rev=2

	{
	  "foo": "B",
	  "_date": "2014-02-05T14:46:07.3761702+01:00",
	  "_id": "1C8F1EA26F7D4225AD4825A536D8D0FD",
	  "_rev": 2
	}

###signalr-interface (expiremental)

	set("counter",132)
	get("counter").done(function(result))