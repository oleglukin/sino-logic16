namespace API.Models
{
    public class SignalEvent
    {
        // TODO follow naming conventions
        public string id_sample { get; set; } // item identifier (76rtw)
        public string num_id { get; set; } // item serial number (ffg#er111)
        public string id_location { get; set; } // location from (3211.2334), can be a name
        public string id_signal_par { get; set; } // sensor generating signal (0xcv11cs)
        public string id_detected { get; set; } // status data (None), - functional, (Nan), - failed
        public string id_class_det { get; set; } // failure type (req11)
    }
}