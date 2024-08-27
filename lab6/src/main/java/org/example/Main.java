package org.example;

import java.io.IOException;
import java.nio.file.Path;

public class Main {
    public static void main(String[] args) throws IOException {
        long time = System.currentTimeMillis();
        Path sourcePath = Path.of("src/main/resources/images");
        Path destinationPath = Path.of("src/main/resources/newImages");
        ImagesController imgController = new ImagesController(sourcePath, destinationPath);
        imgController.transform();
        System.out.println((System.currentTimeMillis() - time)/1000 + " seconds");
    }
}