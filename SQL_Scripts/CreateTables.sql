create table animals (
	AnimalID int auto_increment,
    
    AnimalType varchar(25) not null,
    AnimalName varchar(25) not null,
    Age int not null,
    Weigth int not null,
    Color varchar(25) not null,
    
    check(AnimalType = "Dog" or AnimalType = "Cat" or AnimalType = "Fish" and
          Color = "White" or Color = "Black" or Color = "Beige" or Color = "Grey" or Color = "Blond" or 
          Color = "Red" or Color = "Blue" or Color = "Green" or Color = "Purple"),
    
    primary key(AnimalID)
);

create table owners (
	OwnerID int auto_increment,
    AnimalID int not null,
    
    primary key(OwnerID),
    foreign key(AnimalID) references animals(AnimalID)
);