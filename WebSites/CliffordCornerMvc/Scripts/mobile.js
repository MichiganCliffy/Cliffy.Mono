if (typeof String.prototype.toDate != 'function') {
    // String.toDate() method.
    String.prototype.toDate = function (str) {
        return new Date(parseInt(this.substr(6)));
    };
}

function GroupModel() {
    var self = this;
    self.PageNo = 0;
    self.Thumbnails = ko.observableArray([]);
    self.Photograph = ko.observable();
    self.Tag = "";
    self.ContentUI = null;

    self.ClearThumbnails = function () {
        while (self.Thumbnails().length > 0) {
            self.Thumbnails.pop();
        }
    };

    self.LoadThumbnails = function () {
        var timestamp = new Date().getTime();
        var url = "/services/Photographs";
        var data = ({ "pageNo": self.PageNo, "ts": timestamp });

        if (self.Tag.length > 0) {
            url = "/services/PhotographsByTag";
            data = ({ "tag": self.Tag, "pageNo": self.PageNo, "ts": timestamp });
        }

        $.ajax({
            url: url,
            data: data,
            dataType: "json",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    var item = data[i];
                    self.Thumbnails.push(item);
                }
                self.PageNo++;
            }
        });
    };

    self.LoadSearch = function () {
        self.Tag = self.ContentUI.find("input[name='search']").val();
        self.PageNo = 0;
        self.ClearThumbnails();
        self.LoadThumbnails();
    };

    self.ShowPhotograph = function (model, event) {
        self.ContentUI.find(".thumbnails").slideUp();
        self.ContentUI.find(".photograph").fadeIn();
        self.Photograph(model);
    };

    self.HidePhotograph = function (model, event) {
        self.ContentUI.find(".thumbnails").slideDown();
        self.ContentUI.find(".photograph").fadeOut();
    };
}

function AlbumModel(album, title) {
    var self = this;
    self.Album = album;
    self.AlbumTitle = title;
    self.Thumbnails = ko.observableArray([]);
    self.Photograph = ko.observable();
    self.Tag = "";
    self.Content = ko.observable();
    self.Title = ko.observable(title);
    self.ContentUI = null;

    self.ClearThumbnails = function () {
        while (self.Thumbnails().length > 0) {
            self.Thumbnails.pop();
        }
    };

    self.LoadThumbnails = function () {
        var timestamp = new Date().getTime();
        var url = "/services/Album";
        var data = ({ "album": self.Album, "ts": timestamp });

        if (self.Tag.length > 0) {
            url = "/services/AlbumPage";
            data = ({ "tag": self.Tag, "album": self.Album, "ts": timestamp });
        }

        $.ajax({
            url: url,
            data: data,
            dataType: "json",
            success: function (data) {
                self.Content(data.Content);

                if (self.Tag.length > 0) {
                    self.Title(self.AlbumTitle + ' - ' + data.Title);
                    for (var i = 0; i < data.Photographs.length; i++) {
                        var item = data.Photographs[i];
                        self.Thumbnails.push(item);
                    }
                } else {
                    self.Title(self.AlbumTitle);
                }
            }
        });
    };

    self.LoadTag = function (tag) {
        self.Tag = tag;
        self.ClearThumbnails();
        self.LoadThumbnails();
    };

    self.ShowPhotograph = function (model, event) {
        self.ContentUI.find(".intro").slideUp();
        self.ContentUI.find(".thumbnails").slideUp();
        self.ContentUI.find(".photograph").fadeIn();
        self.Photograph(model);
    };

    self.HidePhotograph = function (model, event) {
        self.ContentUI.find(".intro").slideDown();
        self.ContentUI.find(".thumbnails").slideDown();
        self.ContentUI.find(".photograph").fadeOut();
    };
}