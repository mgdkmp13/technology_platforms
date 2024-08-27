package org.example;
import com.sun.source.tree.Tree;
import org.example.classes.Commandor;
import org.example.classes.CommandorComparator;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        String sortingType;
        if(args.length==0){
            sortingType="normal";
        }
        else{
            sortingType=args[0];
        }

        Commandor Commandor1 = new Commandor("Harry Potter", 18, 23,sortingType);
        Commandor Commandor2 = new Commandor("Hermione", 12, 15,sortingType);
        Commandor Commandor3= new Commandor("Dumbledore", 22, 20,sortingType);
        Commandor Commandor4 = new Commandor("Hagrid", 9, 2,sortingType);
        Commandor Commandor5= new Commandor("Voldemort", 28, 22,sortingType);
        Commandor Commandor6 = new Commandor("McGonall", 16, 17,sortingType);
        Commandor Commandor7 = new Commandor("Draco", 7, 8,sortingType);
        Commandor Commandor8 = new Commandor("Ron", 10, 2,sortingType);
        Commandor Commandor9 = new Commandor("Luna", 9, 10,sortingType);
        Commandor Commandor10 = new Commandor("Zgredek", 1, 1,sortingType);

        Commandor3.addApprentice(Commandor6);
        Commandor3.addApprentice(Commandor4);
        Commandor6.addApprentice(Commandor7);
        Commandor6.addApprentice(Commandor9);
        Commandor5.addApprentice(Commandor10);
        Commandor4.addApprentice(Commandor1);
        Commandor4.addApprentice(Commandor2);
        Commandor4.addApprentice(Commandor8);

        Commandor.showSubordinates(Commandor3, "-");
        //Commandor.showApprentices(Commandor3);

        Commandor4.generateStatistics(sortingType);
    }
}