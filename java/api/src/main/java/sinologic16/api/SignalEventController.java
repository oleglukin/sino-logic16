package sinologic16.api;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class SignalEventController {

    @RequestMapping("api/")
    public String get() {

        return "From SignalEventController.get()";
    }
}