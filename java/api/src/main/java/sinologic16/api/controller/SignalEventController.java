package sinologic16.api.controller;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class SignalEventController {

    @RequestMapping("api/")
    public String get() {

        return "From SignalEventController.get()";
    }
}