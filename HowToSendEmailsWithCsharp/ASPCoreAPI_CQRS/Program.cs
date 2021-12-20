var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// add Email.Net configuration
builder.Services.AddEmailNet(options =>
{
    /* used to specify the default from to be used when sending the emails */
    options.DefaultFrom = new MailAddress("from@email.net");

    /* set the default EDP to be used for sending the emails */
    options.DefaultEmailDeliveryProvider = SmtpEmailDeliveryProvider.Name;

    /* to use socketlabs as the EDP comment the line above, and uncomment this line*/
    //options.DefaultEmailDeliveryProvider = SocketLabsEmailDeliveryProvider.Name;
})
.UseSmtp(options => options.UseGmailSmtp("your-email@gmail.com", "password"))
.UseSocketlabs(apiKey: "", serverId: 0);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
