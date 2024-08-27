package org.example;

import org.apache.commons.lang3.tuple.Pair;

import javax.imageio.ImageIO;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;
import java.util.concurrent.ForkJoinPool;
import java.util.stream.Collectors;
import java.util.stream.Stream;

public class ImagesController {
    private List<Path> files;
    private Path sourcePath;
    private Path destinationPath;
    public ImagesController(Path sourcePath, Path destinationPath){
        this.sourcePath = sourcePath;
        this.destinationPath = destinationPath;
        try{
            Stream<Path> stream = Files.list(this.sourcePath);
            this.files = stream.collect(Collectors.toList());
        } catch (IOException e) {
            System.out.println("Problem with opening files");
        }
    }

    public void transform() throws IOException{
        int threadsNumber = 15;
        ForkJoinPool forkJoinPool = new ForkJoinPool(threadsNumber);

        forkJoinPool.submit(() ->
            this.files.parallelStream().map(file -> {
                BufferedImage image = null;
                try {
                    image = ImageIO.read(file.toFile());
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
                String name = String.valueOf(file.getFileName());
                return Pair.of(name, image);
            }).map(pair->{
                BufferedImage original = pair.getRight();
                BufferedImage image = new BufferedImage(original.getWidth(),
                        original.getHeight(),
                        original.getType());
                for (int i = 0; i < original.getWidth(); i++) {
                    for (int j = 0; j < original.getHeight(); j++) {
                        int rgb = original.getRGB(i, j);
                        Color color = new Color(rgb);
                        int red = color.getRed();
                        int blue = color.getBlue();
                        int green = color.getGreen();
                        Color outColor = new Color(red, blue, green);
                        int outRgb = outColor.getRGB();

                        image.setRGB(i, j, outRgb);
                    }
                }
                String name = pair.getLeft();
                return Pair.of(name, image);
            }).forEach(pair -> {
                String name = pair.getLeft();
                File outputfile = new File(String.valueOf(Path.of(this.destinationPath + "/" + name)));
                try {
                    ImageIO.write(pair.getRight(), "jpg", outputfile);
                } catch (IOException e) {
                    throw new RuntimeException(e);
                }
            })
        ).join();

        forkJoinPool.shutdown();
    }
}
