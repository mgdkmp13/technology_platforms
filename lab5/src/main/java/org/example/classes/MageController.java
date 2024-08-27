package org.example.classes;
import java.util.Optional;

import static org.junit.Assert.assertFalse;

public class MageController {
    private MageRepository repository;
    public MageController(MageRepository repository) {
        this.repository = repository;
    }
    public String find(String name) {
            Optional<Mage> findMage = this.repository.find(name);

            if (findMage.isPresent()) {
                return "done";
            }
        return "not found";
    }
    public String delete(String name) {
        Optional<Mage> findMage = this.repository.find(name);
        if(findMage.isPresent()) {
            this.repository.delete(name);
            return "done";
        }
        return "not found";
    }
    public String save(String name, String level) {
        Optional<Mage> findMage = this.repository.find(name);
        if(findMage.isEmpty()) {
            this.repository.save(new Mage(name, Integer.parseInt(level)));
            return "done";
        }
        return "bad request";
    }
}
