# sino-logic16

This project is created for education purpose. Model and test events processing.

## .Net based solution
There is a web API that accepts signals and runs stream queries using NEsper library.
Currently it support the following analytics query: calculate number of functional and failed signals grouped by location

API methods:
1. Receive signal (POST). This can be ingested from another system, e.g. Logstash.
2. Calculate failed and functional signals for all locations (GET).
3. Get results by location, same as #2 but outputs results for one requested location.
4. Reset aggregation for all locations to 0 (DELETE).
5. Reset aggregation by location, same as #4 but limit aggregation reset by one location.

Signal schema:

    {
      "id_sample": "95ggm", 	// item identifier
      "num_id": "fcc#wr995", 	// item serial number
      "id_location": "1564.9956", 	// location id, can be a name
      "id_signal_par": "0xvckkep",	// sensor generating signal
      "id_detected": "None", 	// status data (None), - functional, (Nan), - failed
      "id_class_det": "req44"	// failure type
    }

EPL query used for aggregation:

    select id_location, id_detected, count(*)
	from SignalEvent#time_batch(2 sec)
	group by id_location, id_detected

It collects events during 2 seconds time interval and then provides results to the aggregation listener. Results are grouped by location and status (`id_detected`).


### EventSource
There is also a console app to test the API: **EventSource** project.
Update configuration (appsettings.json) and run the app to post sample events to API:

    {
      "ApiEndpoint": "http://localhost:5000/api/signalevent",
      "locations": "3211.2334, bowen,kurashiki, 98444561, 11000101,42,nowhere, 129.6481",
      "eventsToSend": 20
    }

It posts random events using configuration from that json file.
I did the following test on my laptop:
- Sent events using the configuration above, but changed `eventsToSend` to 100,000.
  - It took about 15-20 seconds to post them all
- Sent Get request to check aggregations straight after that
  - Verified that total events number is equal to 100,000.


## References
- [Esper Documentation](http://www.espertech.com/esper/)
- [EsperTech Inc. GitHub profile](https://github.com/espertechinc)