﻿using AlbyOnContainers.ProductDataManager.Domain;
using Ardalis.Specification.EntityFrameworkCore;

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Repositories;

public class CategoryRepository(ProductContext context) : RepositoryBase<Category>(context);
