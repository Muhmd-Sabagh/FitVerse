        /// User.Identity.(any feature) ===> 'User' read from 'claims' by 'identity' 
        code:
        => User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.id)
        
        /// get data from cookie(claims)
        code:   
        => User.Identity.Name