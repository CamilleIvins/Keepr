// extend, as in InstaCult
export class Profile{
  constructor(data)
  {
    this.id = data.id
    this.name = data.name
    this.picture = data.picture
    this.coverImg = data.coverImg || 'https://images.unsplash.com/photo-1496843916299-590492c751f4?auto=format&fit=crop&q=80&w=2071&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D'
  }
}

export class Account  extends Profile{
  constructor(data) {
    super(data)
    this.email = data.email
  
    // TODO add additional properties if needed: COVERIMG
  }
}
