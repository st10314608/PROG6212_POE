public async Task<List<Claim>> GetClaimsForRole(string role)
{
    switch (role)
    {
        case "Coordinator":
            return await _context.Claims.Where(c => c.Status == "Pending").ToListAsync();
        case "Manager":
            return await _context.Claims.Where(c => c.Status == "Verified").ToListAsync();
        default:
            return new List<Claim>();
    }
}
