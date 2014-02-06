##Restful and SignalR interfaces for persistence of any json data - experimental

For .net. Inspired by "Nobackend", CouchDb and Firebase. A hobby implementation useful for experimentation.

Just run the solution from command line and you get a rest interface on localhost:8080 for any json data.

Or go to /index.html to get a html page with a small signalr sample - and use the console from there.

Alternatively - run Almhult.AspNet or publish it to IIS.

The data stores as Json in Esent (the native Windows data sln) - in App_Data folders (one per resource).

No built in security ("admin-party" mode).

The main functionality is in https://github.com/joeriks/PersistenceRest (nuget PersistenceRest.EsentJson.Owin).

###REST-interface

	/{resource} : gets all items
	/{resource}/{id} : gets one resource item
	/{resource} (data) [post] : creates new item
	/{resource}/{id} (data) [put] : updates item
	/{resource} [delete] : deletes all items in resource collection

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

	The resource has two stored items, the "BC" one is the 3rd revision with that id.

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

###signalr-interface

	A SignalR-interface for the data, connect to it from the js console at localhost:8080/index.html

	// returns new id
	set(object)
	set(id, object)
	
	// void
	clear()

	// returns promise
	get(id) 
	getValue(id) : gets only value (no id, date and rev info)
	get(id, revision)
	getAll() : gets all
	getAll(true) : gets all, without id, date and rev info
