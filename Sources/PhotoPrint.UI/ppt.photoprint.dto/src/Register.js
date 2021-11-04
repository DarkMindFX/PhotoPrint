
class RegisterRequest {

    set user(val) { this.User = val; }
    get user()    { return this.User; }

    set contact(val)   { this.Contact = val; }
    get contact()      { return this.Contact; }

}

class RegisterResponse {

    set user(val) { this.User = val; }
    get user()    { return this.User; }

    set contact(val)   { this.Contact = val; }
    get contact()      { return this.Contact; }
}

module.exports = {
    RegisterRequest,
    RegisterResponse
}