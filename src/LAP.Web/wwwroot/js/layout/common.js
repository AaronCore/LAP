var common = {
    deletes: function (the, url, param) {
        the.$msgbox({
            title: '提示',
            type: 'warning',
            message: '此操作将永久删除该数据, 是否继续?',
            showCancelButton: true,
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            beforeClose: function (action, instance, done) {
                if (action === 'confirm') {
                    instance.confirmButtonLoading = true;
                    instance.confirmButtonText = '执行中...';
                    $.post(url, param, function (res) {
                        if (res === 1) {
                            api.showMsg("删除成功...", "success");
                            the.getTableData();
                        } else {
                            api.showMsg("删除失败，详情请查看错误日志...", "error");
                        }
                        done();
                        instance.confirmButtonLoading = false;
                    });
                } else {
                    done();
                }
            }
        });
    },
    enableds: function (the, url, param) {
        the.$msgbox({
            title: '提示',
            type: 'warning',
            message: '此操作将修改数据状态, 是否继续?',
            showCancelButton: true,
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            beforeClose: function (action, instance, done) {
                if (action === 'confirm') {
                    instance.confirmButtonLoading = true;
                    instance.confirmButtonText = '执行中...';
                    $.post(url, param, function (res) {
                        if (res === 1) {
                            api.showMsg("修改成功...", "success");
                            the.getTableData();
                        } else {
                            api.showMsg("修改失败，详情请查看错误日志...", "error");
                        }
                        done();
                        instance.confirmButtonLoading = false;
                    });
                } else {
                    done();
                }
            }
        });
    }
};