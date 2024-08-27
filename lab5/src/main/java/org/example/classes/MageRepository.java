package org.example.classes;
import static org.junit.Assert.assertFalse;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Optional;

public class MageRepository {
    private Collection<Mage> collection;

    public MageRepository() {
        this.collection = new ArrayList<>();
    }

    public Optional<Mage> find(String name) {
           // if(!this.collection.isEmpty()) {
            for (Mage m : this.collection) {
                if (m.getName().equals(name)) {
                    return Optional.of(m);
                }
            }
        //}
        return Optional.empty();
    }
    public void delete(String name) {
        Optional<Mage> findMage = this.find(name);
        if (findMage.isPresent()) {
            this.collection.remove(findMage.get());
        } else {
            throw new IllegalArgumentException("Mage with name " + name + " not found");
        }
    }

    public void save(Mage mage) {
        Optional<Mage> findMage = this.find(mage.getName());
        if (findMage.isEmpty()){
            this.collection.add(mage);
        }
        else {
            throw new IllegalArgumentException("Mage with name " + mage.getName() + " not found");
        }
    }

    public Collection<Mage> getCollection() {
        return collection;
    }

    public void setCollection(Collection<Mage> collection) {
        this.collection = collection;
    }
}

