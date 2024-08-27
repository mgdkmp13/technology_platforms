package org.example;

import javax.persistence.*;
import java.awt.*;
@Entity
public class Mage {
    @Id
    private String name;
    private int level;
    @ManyToOne
    private Tower tower;

    public Mage() {
        this.name = "Name";
        this.level = 0;
        this.tower = null;
    }

    public Mage(String name, int level, Tower tower) {
        this.name = name;
        this.level = level;
        this.tower = tower;
        tower.getMages().add(this);

    }

    public String toString(){
        return this.name + ", " + this.level + ", " + this.tower.getName();
    }

    public String getName() {
        return name;
    }

    public int getLevel() {
        return level;
    }

    public Tower getTower() {
        return tower;
    }

    public void setName(String name) {
        this.name = name;
    }

    public void setLevel(int level) {
        this.level = level;
    }

    public void setTower(Tower tower) {
        this.tower = tower;
    }


}