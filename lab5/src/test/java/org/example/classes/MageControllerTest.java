package org.example.classes;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.*;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;

class MageControllerTest {

    @Mock
    private MageRepository repoMock;

    @InjectMocks
    private MageController controller;

    @BeforeEach
    void init() {
        this.repoMock = mock(MageRepository.class);
        when(repoMock.find("Madzia")).thenReturn(Optional.of(new Mage("Madzia", 10)));
        when(repoMock.find("Marysia")).thenReturn(Optional.of(new Mage("Zuzia", 4)));
        this.controller = new MageController(repoMock);
    }

    @Test
    void testFindNonExistentMage() {
        String result = controller.find("NonExistentMage");
        assertEquals(result, "not found");
    }

    @Test
    void testFindExistentMage() {
        String result = controller.find("Madzia");
        assertEquals("done", result);
    }

    @Test
    void testDeleteNonExistentMage() {
        String result = controller.delete("NonExistentMage");
        assertEquals("not found", result);
    }

    @Test
    void testDeleteExistentMage() {
        String result = controller.delete("Marysia");
        assertEquals("done", result);
    }

    @Test
    void testSaveNonExistentMage() {
        String result = controller.save("Zuzia", "7");
        assertEquals("done", result);
    }

    @Test
    void testSaveExistentMage() {
        String result = controller.save("Madzia", "10");
        assertEquals("bad request", result);
    }
}