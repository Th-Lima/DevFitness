using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.ViewModels;

namespace DevFitness.API.Profiles
{
    public class MealProfile : Profile
    {
        public MealProfile()
        {
            CreateMap<Meal, MealViewModel>();
        }
    }
}
