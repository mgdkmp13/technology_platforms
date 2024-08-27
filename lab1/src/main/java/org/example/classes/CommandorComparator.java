package org.example.classes;

import java.util.Comparator;

public class CommandorComparator implements Comparator<Commandor> {

    @Override
    public int compare(Commandor o1, Commandor o2) {
        // Alternatywny porządek: level, name, power
        int isSame = Integer.compare(o1.getLevel(), o2.getLevel());

        if (isSame == 0) {
            isSame = o1.getName().compareTo(o2.getName());
            if (isSame == 0) {
                isSame = Double.compare(o1.getPower(), o2.getPower());
            }
        }
        return isSame;
    }

}
