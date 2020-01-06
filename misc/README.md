# Logstash configuration examples

## Signal Events Ingestion into ElasticSearch and Event Processing API
See [logstash-signal-input-signal.conf](https://github.com/oleglukin/sino-logic16/blob/master/misc/logstash-signal-input-signal.conf)
This would listen on HTTP port, apply some filtering and output data to 2 destinations:
1. ElasticSearch index for raw time-series data
2. API endpoint for events processing

## Results output
See [logstash-results-output.conf](https://github.com/oleglukin/sino-logic16/blob/master/misc/logstash-results-output.conf)
This simply outputs event processing results to 2 destinations:
1. Another ElasticSearch index for future analysis
2. Websocket for reporting