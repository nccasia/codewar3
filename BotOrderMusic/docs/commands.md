# Commands

Bot chỉ thực hiện các lệnh sau. Lưu ý các lệnh phải có tiền tố là “!music”
## Public Commands
- help: hiển thị các lệnh có thể thực thi với bot

- order <youtubeUrl>: order nhạc bằng cách gửi link youtube

- tracklists: hiển thị danh sách các bài đã order
 
## Private Commands
- block <username>: chặn không cho user order nhạc.

- close: kết thúc việc order nhạc, bot sẽ không nhận bất cứ order nào cho đến lần lập lịch tiếp theo.

- reject <username> <number> <...reason>: loại bỏ 1 bài hát của 1 user ra khỏi playlist theo thứ tự order của user đấy.

- deleteAll: xoá tất cả các bài hát có trong danh sách chờ

- next: đi đến bài hát tiếp theo
 
## Optional:
- vote <number>: vote theo mã số bài hát
- favoritelists: hiển thị danh sách các bài hát yêu thích nhất

- wakeup: kích hoạt bot trước thời gian đã lập lịch

