

var myAllowSpecificOrigins = "_myAllowSpecificOrigins"; //Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core
//
//Ứng dụng web được sử dụng để định cấu hình đường dẫn HTTP và các tuyến đường.
//  Khởi tạo phiên bản mới của Microsoft.AspNetCore.Builder.WebApplicationBuilder
// lớp có giá trị mặc định được định cấu hình trước.
var builder = WebApplication.CreateBuilder(args);


// Add services to the container. Thêm dịch vụ vào vùng chứa.

builder.Services.AddControllers();

//
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//// AddEndpointsApiExplorer dùng Định cấu hình ApiExplorer bằng Microsoft.AspNetCore.Http.Endpoint.Metadata
// ApiExplorer Trình khám phá này cho phép người dùng điều hướng API bằng các siêu liên kết.
//Tài nguyên có thể được tạo, truy xuất, sửa đổi và xóa trực tiếp từ công cụ này.
//Theo mặc định, có thể truy cập API Explorer bằng cách duyệt https://localhost:55539 trên bất kỳ máy nào đã cài đặt API.
//Giao diện này cũng là nơi mã thông báo truy cập được tạo.
builder.Services.AddEndpointsApiExplorer();





builder.Services.AddSwaggerGen();// thêm dịch vụ Swagger
//
//Swagger Swagger là một Ngôn ngữ mô tả giao diện để mô tả các API RESTful được thể hiện bằng JSON.
//Swagger được sử dụng cùng với một bộ công cụ phần mềm nguồn mở để thiết kế,
//xây dựng, lập tài liệu và sử dụng các dịch vụ web
//


// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
        ////Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline. // định cấu hình đường dẫn yêu cầu HTTp
if (app.Environment.IsDevelopment())
{
    //nếu môi trường hiện tại được đặt thành Phát triển.
    //Lệnh UseSwaggerUI gọi phương thức kích hoạt Phần mềm trung gian tệp tĩnh 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myAllowSpecificOrigins);////Bật yêu cầu đa nguồn (CORS) trong ASP.NET Core

app.UseHttpsRedirection();//Thêm phần mềm trung gian để chuyển hướng Yêu cầu HTTP sang HTTPS.
//Sử dụng phương pháp Https Redirection.
//Nó đưa ra mã phản hồi HTTP chuyển hướng từ http sang https

//
//Authorization ( ủy quyền) là quá trình cho phép người dùng đã xác thực truy cập vào các tài nguyên.
// AuthorizationMiddleware: là lớp Ủy quyền trung gian => 

app.UseAuthorization();//// Thêm Microsoft.AspNetCore.Authorization.AuthorizationMiddleware vào phần đã chỉ định Microsoft.AspNetCore.Builder.IApplicationBuilder, cho phép ủy quyền khả năng.
// Khi cấp quyền cho một tài nguyên được định tuyến bằng cách sử dụng định tuyến điểm cuối, lệnh gọi này
// phải xuất hiện giữa các lệnh gọi đến app.UseRouting () và app.UseEndpoints (...) cho phần mềm trung gian hoạt động chính xác.
// 

app.MapControllers();
//Thêm điểm cuối cho các hành động của bộ điều khiển vào Microsoft.AspNetCore.Routing.IEndpointRouteBuilder mà không chỉ định bất kỳ tuyến đường nào.

// Endpoint Route Builder là Quy trình mà ASP.NET Core kiểm tra các yêu cầu HTTP đến và ánh xạ chúng tới Điểm cuối thực thi của ứng dụng .
// Chúng xác định Điểm cuối trong quá trình khởi động ứng dụng.
// Sau đó, Mô-đun định tuyến đối sánh URL đến với một Điểm cuối và gửi yêu cầu đến nó.

app.Run();
//Chạy ứng dụng và chặn chuỗi cuộc gọi cho đến khi máy chủ tắt.
