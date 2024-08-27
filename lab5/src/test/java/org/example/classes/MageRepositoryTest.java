package org.example.classes;

import org.junit.Assert;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

class MageRepositoryTest {
    private MageRepository repo;

    @BeforeEach
    void addTestMages(){
        this.repo = new MageRepository();

        Mage m1 = new Mage("Madzia", 10);
        repo.save(m1);

        Mage m2 = new Mage("Marysia", 3);
        repo.save(m2);
    }


    @Test
    void testFindNonExistentMage() {
        Optional<Mage> findMage = this.repo.find("orfchiehfci");
        assertTrue(findMage.isEmpty(), "Expected Optional<Mage> to be empty");
    }

    @Test
    void testFindExistentMage() {
        Optional<Mage> findMage = this.repo.find("Madzia");
        assertTrue(findMage.isPresent(), "Expected Optional<Mage> to be present");
    }

    @Test
    void testDeleteNonExistentMage() {
        assertThrows(IllegalArgumentException.class, () -> this.repo.delete("orfchiehfci"));
    }

    @Test
    void testDeleteExistentMage() {
        assertTrue(this.repo.find("Madzia").isPresent());
        this.repo.delete("Madzia");
        assertFalse(this.repo.find("Madzia").isPresent());
    }

    @Test
    void testSaveNonExistentMage() {
        assertFalse(this.repo.find("Zuzia").isPresent());
        this.repo.save(new Mage("Zuzia", 6));
        assertTrue(this.repo.find("Zuzia").isPresent());
    }

    @Test
    void testSaveExistentMage() {
        assertThrows(IllegalArgumentException.class, () -> this.repo.save(new Mage("Madzia", 6)));
    }
}