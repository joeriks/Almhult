basic idea:

###rest-interface

/set/counter/132
/get/counter

###signalr-interface

set("counter",132)
get("counter").done(function(result))