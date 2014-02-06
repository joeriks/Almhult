##Restful storage middleware for Owin

Implements Owin.IAppBuilder

usage

    app.Map("/rest", map =>
    {
        map.UsePersistenceRest(new PersistenceRestMiddleware.PersistenceRestOptions {
            RestStorage = new PersistenceRestEsentJsonStorage()
        });
    });

where RestStorage implements IPersistenceRestStorage

for use with Owin