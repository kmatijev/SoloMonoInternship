// js objects

function Fruit(theColor, theSweetness, theFruitName, theNativeToLand) {

    this.color = theColor;
    this.sweetness = theSweetness;
    this.fruitName = theFruitName;
    this.nativeToLand = theNativeToLand;
  
    this.showName = function() {
      console.log("This is a " + this.fruitName);
    }
  
    this.nativeTo = function() {
      this.nativeToLand.forEach(function(eachCountry) {
        console.log("Grown in:" + eachCountry);
      });
    }
}
var mangoFruit = new Fruit ("Yellow", 8, "Mango", ["South America", "Central America", "West Africa"]);

//console.log(mangoFruit.nativeToLand[0]);

// prototypes

function FruitPrototype () {

}

FruitPrototype.prototype.color = "Yellow";
FruitPrototype.prototype.sweetness = 7;
FruitPrototype.prototype.fruitName = "Mango";
FruitPrototype.prototype.nativeToLand = "USA";

FruitPrototype.prototype.showName = function () {
console.log("This is a " + this.fruitName);
}

FruitPrototype.prototype.nativeTo = function () {
            console.log("Grown in:" + this.nativeToLand);
}

var mangoFruitProto = new FruitPrototype ();
mangoFruitProto.showName();
mangoFruitProto.nativeTo();

function HigherLearning () {
    this.educationLevel = "University";
    }
    
var school = new HigherLearning ();
school.schoolName = "MIT";
school.schoolAccredited = true;
school.schoolLocation = "Massachusetts";

for(eachProp in school)
{
    console.log(eachProp);
}

// prototype inheritance -> oop
function Plant () {
    this.country = "Mexico";
    this.isOrganic = true;
    }
    
    Plant.prototype.showNameAndColor =  function () {
    console.log("I am a " + this.name + " and my color is " + this.color);
    }
    
    Plant.prototype.amIOrganic = function () {
    if (this.isOrganic)
    console.log("I am organic, Baby!");
    }
    
    function Fruit (fruitName, fruitColor) {
    this.name = fruitName;
    this.color = fruitColor;
    }
    
    Fruit.prototype = new Plant ();
    
    var aBanana = new Fruit ("Banana", "Yellow");
    
    console.log(aBanana.name); // jer smo stavili da je fruit prototype new plant, moze accesat sve metode i propertye iz Plant i Fruit
    
    console.log(aBanana.showNameAndColor());

function showOrdinaryPersonName () {    
    var name = "Johnny Evers"; // kad declareamo unutar funckcije onda je scope local a ne global
    console.log (name);
}