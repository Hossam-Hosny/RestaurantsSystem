﻿using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs;

public class DishesProfile:Profile
{
    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();

        CreateMap<CreateDishCommand, Dish>(); 
    }


}
