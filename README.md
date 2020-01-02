# sino-logic16

This project is created for education purpose. Model and test events processing.

## .Net based solution
There is a web API that accepts signals and runs stream queries using NEsper library.
Currently it support the following analytics query: calculate number of functional and failed signals grouped by location

API methods:
1. Receive signal (POST). This can be injected from another system, e.g. Logstash.
2. Calculate failed and functional signals for all locations (GET).
3. Get results by location, same as #2 but outputs results for one requested location.
4. Reset aggregation for all locations to 0

Signal schema:

    {
      "id_sample": "95ggm", 	// item identifier
      "num_id": "fcc#wr995", 	// item serial number
      "id_location": "1564.9956", 	// location, can be a name
      "id_signal_par": "0xvckkep",	// sensor generating signal
      "id_detected": "None", 	// status data (None), - functional, (Nan), - failed
      "id_class_det": "req44"	// failure type (req11)
    }




## References
- [Esper Documentation](http://www.espertech.com/esper/)
- [EsperTech Inc. GitHub profile](https://github.com/espertechinc)