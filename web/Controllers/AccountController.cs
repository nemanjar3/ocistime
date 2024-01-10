using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult SignIn(string email, string password)
    {
        // Implement sign-in logic here
        // Validate user credentials and sign in the user

        // Redirect to another page after successful sign-in
        return RedirectToAction("Index", "Home");
    }
}
