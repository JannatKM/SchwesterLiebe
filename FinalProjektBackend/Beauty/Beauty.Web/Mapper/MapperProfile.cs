using AutoMapper;
using Beauty.Entity.Entities;
using Beauty.Shared.DTOs.Appointment;
using Beauty.Shared.DTOs.AppointmentType;
using Beauty.Shared.DTOs.Booking;
using Beauty.Shared.DTOs.Customer;
using Beauty.Shared.DTOs.Discount;
using Beauty.Shared.DTOs.Employee;
using Beauty.Shared.DTOs.EmployeeCalendar;
using Beauty.Shared.DTOs.EmployeeTime;
using Beauty.Shared.DTOs.Product;
using Beauty.Shared.DTOs.Room;
using Beauty.Shared.DTOs.User;

namespace Beauty.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Appointment, AppointmentDto>().ReverseMap();

            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, RoomCreationDto>().ReverseMap();
            CreateMap<Room, RoomEditionDto>().ReverseMap();

            CreateMap<AppointmentType, AppointmentTypeDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<EmployeeCalendar, EmployeeCalendarDto>().ReverseMap();
            CreateMap<EmployeeCalendar, EmployeeCalendarForCreationDto>().ReverseMap();
            CreateMap<EmployeeCalendar, EmployeeCalendarForEditionDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, BookingCreationDto>().ReverseMap();

            CreateMap<Discount, DiscountDto>().ReverseMap();

            CreateMap<EmployeeTime, EmployeeTimeDto>().ReverseMap();
        }
    }
}
