﻿using Ardalis.Specification;

namespace MyVocabulary.Domain.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{ }