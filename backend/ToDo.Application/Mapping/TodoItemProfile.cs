using System;
using System.Collections.Generic;

using ToDo.Application.DTOs;
using ToDo.Domain.Entities;
using AutoMapper;


namespace ToDo.Application.Mapping
{
    public class TodoItemProfile:Profile
    {
        public TodoItemProfile()
        {
            CreateMap<ToDoItem, TodoItemDto>().ReverseMap();
            CreateMap<TodoItemCreateDto, ToDoItem>();
            CreateMap<TodoItemUpdateDto, ToDoItem>();
        }
    }
}
