services.AddDbContext<ClaimDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
