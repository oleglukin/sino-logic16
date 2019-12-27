namespace API.Models
{
    public class SignalEvent
    {
        // TODO follow naming conventions
        public string id_sample { get; set; } // идетнификатор предмета(76rtw)
        public string num_id { get; set; } // серийный номер предмета(ffg#er111)
        public string id_location { get; set; } // место откуда(3211.2334), а может быть(Екатеринбург)
        public string id_signal_par { get; set; } // датчик, с которого идет сигнал(0xcv11cs)
        public string id_detected { get; set; } // данные о состоянии(None), - исправен, (Nan), - поломка
        public string id_class_det { get; set; } // вид поломки(req11)
    }
}