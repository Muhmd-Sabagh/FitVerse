        /// User.Identity.(any feature) ===> 'User' read from 'claims' by 'identity' 
        code:
        => User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.id)
      ===============================================================================
        /// get data from cookie(claims)
        code:   
        => User.Identity.Name
        ===============================================================================
        ///create cookie and save data of user in claims 
        code:
        ==>SignInManager(object).SignInAsync
        example:
        ==>await signInManager.SignInAsync(userFromDb,userFromReq.RememberMe);
        ================================================================================
        
        ///custom claims for extra info
        code:
        ==>     List<Claim> claims=new List<Claim>();
                claims.Add(new Claim('type',value));
                signInManager.SignInWithClaimsAsync(userFromDb,userFromReq.RememberMe,)
    =======================================================================================
    //assign user to role
    code:
    ==> await userManager.AddToRoleAsync(user, "admin2");