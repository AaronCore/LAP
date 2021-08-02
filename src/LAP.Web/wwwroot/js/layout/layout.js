var api = {
    // 1.提示框
    showMsgByObj: function ({
        type,
        code,
        msg
    }) {
        // 传入对象提示消息
        this.showMsg(msg, type);
    },
    showMsg: function (msg, type) {
        // 传入参数显示消息
    },
    showMsgByHtml: function (title, html) {
        // 提示HTML内容
    },
    // 2.弹出窗
    // 3.子页面参数
    getTabParams: function (tabKey) {
        // 子页面的Key传进来
    },
    setTabParams: function (tabKey, params) {
        // 子页面的Key和params对象传进来
    },
    // 4.tabs操作
    jumpTabByObj: function ({
        id,
        title,
        url,
        icon
    }) {
        jumpTab(id, title, url, icon);
    },
    jumpTab: function (id, title, url, icon) {

    },
    tabCloseOrCallBack: function (tab, chain) {
        if (tab) {
            if (typeof chain === 'undefined') {
                // 传个空的防止出现 undefined
                chain = new SalChain([], null);
            }
            var cType = typeof tab.callback;
            if (cType === 'function') {
                tab.callback(tab, chain);
            } else {
                this.closeTab(tab);
                chain.next();
            }
        }
    },
    closeTab: function (tab) {

    },
    // 5.全屏loading遮罩
    openLoading: function (time) {
        // 打开遮罩,传入自动取消时间
    },
    closeLoading: function () {
        // 手动关闭遮罩
    },
    // 6.检查是否拥有资源，本质上是从menuArr中查找出来
    cr: function (key) {
        // Check Resource 的简写
    },
    getCurrTabRes: function (iframWindow) {
        // 获取当前tab页对应的资源
    }
};

var homeTab = {
    key: 'home',
    name: 'home',
    title: '',
    show: true,
    url: '/Dashboard/Index',
    icon: 'layui_icon font-size16 layui_icon_home',
    notClosable: true
};

new Vue({
    el: '#homeApp',
    data() {
        return {
            isCollapse: false,
            gobalParams: gobal.params,
            loadding: {},
            menuDefaultActive: 'useDocTemplate',
            menuArr: [
                {
                    key: 'baseManage',
                    title: '基础管理',
                    show: true,
                    url: '#',
                    icon: 'el-icon-s-tools',
                    children: [
                        {
                            key: 'mouduleList',
                            title: '模块列表',
                            show: true,
                            url: '/Module/Index',
                            icon: 'el-icon-menu',
                        },
                    ]
                },
                {
                    key: 'logManage',
                    title: '日志分析',
                    show: true,
                    url: '#',
                    icon: 'el-icon-s-management',
                    children: [
                        {
                            key: 'logDashboard',
                            title: '仪表盘',
                            show: true,
                            url: '/Logger/Dashboard',
                            icon: 'el-icon-s-platform',
                        },
                        {
                            key: 'loggerList',
                            title: '日志管理',
                            show: true,
                            url: '/Logger/Index',
                            icon: 'el-icon-menu',
                        }
                    ]
                },
                {
                    key: 'statisticLogManage',
                    title: '统计分析',
                    show: true,
                    url: '#',
                    icon: 'el-icon-s-data',
                    children: [
                        {
                            key: 'statisticLogDashboard',
                            title: '仪表盘',
                            show: true,
                            url: '/StatisticLog/Dashboard',
                            icon: 'el-icon-s-platform',
                        },
                        {
                            key: 'statisticLogList',
                            title: '请求日志',
                            show: true,
                            url: '/StatisticLog/Index',
                            icon: 'el-icon-menu',
                        }
                    ]
                },
                {
                    key: 'warningManage',
                    title: '预警管理',
                    show: true,
                    url: '#',
                    icon: 'el-icon-message-solid',
                },
            ],
            tabItems: [homeTab],
            tabActive: homeTab.key,
            tabsPopper: {
                id: homeTab.key,
                isShow: false,
                top: '0px',
                left: '0px'
            },
            tabParams: {}
        }
    },
    created: function () {
        var _this = this;
        // 重写api
        // 1.提示框
        // type：成功(success),警告(warning),错误(error)
        api.showMsg = function (msg, type = null) {
            if (type == null) {
                _this.$message({ showClose: true, message: msg, showClose: true });
            } else {
                _this.$message({ showClose: true, message: msg, type: type, showClose: true });
            }
        };
        api.showMsgByHtml = function (title, html) {
            _this.$notify({ title: title, dangerouslyUseHTMLString: true, message: html });
        };
        // 2.弹出窗----需要固定自定义的,先不做统一接口
        // 3.子页面参数
        api.getTabParams = function (tabKey) {
            //子页面的Key传进来
            return _this.tabParams[tabKey];
        };
        api.setTabParams = function (tabKey, params) {
            //子页面的Key和params对象传进来
            _this.tabParams[tabKey] = params;
        };
        // 4.tabs操作
        api.jumpTab = function (key, title, url, icon) {
            var activeName = _this.tabActive;
            if (key !== activeName) {
                var tabs = _this.tabItems.filter(function (tab) {
                    return tab.name === key;
                });
                if (tabs.length > 0) {
                    activeName = key;
                } else {
                    _this.tabItems.push({ title: title, name: key, url: url, icon: icon });
                    activeName = key;
                }
                _this.tabActive = activeName;
            }
        };
        api.closeTab = function (tab) {
            if (tab) {
                var cType = typeof tab;
                var targetName;
                if (cType === 'string') {
                    targetName = tab;
                } else if (tab.name) {
                    targetName = tab.name;
                } else {
                    return;
                }
                var tabs = _this.tabItems;
                var activeName = _this.tabActive;
                if (activeName === targetName) {
                    tabs.forEach((tab, index) => {
                        if (tab.name === targetName) {
                            var nextTab = tabs[index + 1] || tabs[index - 1];
                            if (nextTab) {
                                activeName = nextTab.name;
                            }
                        }
                    });
                }
                _this.tabActive = activeName;
                _this.tabItems = tabs.filter(tab => tab.name !== targetName);
            }
        };
        // 5.全屏loading遮罩
        api.openLoading = function (time) {
            _this.loading = _this.$loading({ lock: true, text: 'Loading', spinner: 'el-icon-loading', background: 'rgba(0, 0, 0, 0.7)' });
            if (time) {
                setTimeout(function () { _this.loading.close(); }, time);
            }
        };
        api.closeLoading = function () {
            _this.loading.close();
        };
        // 6.检查是否拥有资源，本质上是从menuArr中查找出来
        api.cr = function (key) {
            // Check Resource 的简写
            return _this.findMenuObj(key, _this.menuArr);
        };
        api.getCurrTabRes = function (iframWindow) {
            // 获取当前tab页对应的资源
            var name = iframWindow.name;
            if (name) {
                // iframe_name_xxxx
                var key = name.substring(12);
                return _this.findTab(key, _this.tabItems);
            }
            return false;
        };
    },
    watch: {},
    updated: function () {
        var _this = this;
        _this.$nextTick(function () { _this.bindTabTilesOncontextmenu(); });
    },
    methods: {
        signOut: function () {
            $.get("/Login/SignOut", function () {
                window.location.href = "/Login/Index";
            });
        },
        hasShowItem: function (arr) {
            if (arr && arr.length > 0) {
                return arr.filter(function (e) { return e.show; });
            }
            return [];
        },
        findMenuObj: function (key, arr) {
            if (arr && arr.length > 0) {
                for (var i = 0; i < arr.length; i++) {
                    if (key === arr[i].key) {
                        return arr[i];
                    } else {
                        var result = this.findMenuObj(key, arr[i].children);
                        if (result !== false) {
                            return result;
                        }
                    }
                }
            }
            return false;
        },
        findTab: function (name, arr) {
            if (arr && arr.length > 0) {
                for (var i = 0; i < arr.length; i++) {
                    if (name === arr[i].name) {
                        return arr[i];
                    }
                }
            }
            return false;
        },
        findMenuParent: function (key, arr) {
            if (arr && arr.length > 0) {
                for (var i = 0; i < arr.length; i++) {
                    if (key === arr[i].key) {
                        return true;
                    } else {
                        var result = this.findMenuParent(key, arr[i].children);
                        if (result === true) {
                            return arr[i];
                        } else if (result !== false) {
                            return result;
                        }
                    }
                }
            }
            return false;
        },
        handleSelect: function (key) {
            var curr = this.findMenuObj(key, this.menuArr);
            // console.log(curr);
            if (curr !== false) {
                api.jumpTab(curr.key, curr.title, curr.url, curr.icon);
            }
        },
        bindTabTilesOncontextmenu: function () {
            // 绑定tabs右键菜单
            var _this = this;
            var tabTiles = gobal.utils.getDoms(/^tab-.*/);
            document.onmousedown = function (e) {
                if (e.button === 2 || e.button === 0 || e.button === 1) {
                    _this.tabsPopper.isShow = false;
                }
            };
            for (var i = 0; i < tabTiles.length; i++) {
                var obj = tabTiles[i];
                obj.oncontextmenu = function (e) {
                    _this.tabsPopper.isShow = true;
                    _this.tabsPopper.id = this.getAttribute('id').replace("tab-", "");
                    _this.tabsPopper.top = (e.y - 6) + 'px';
                    _this.tabsPopper.left = ((_this.gobalParams.windowFullWidth - e.x) < 110 ? _this.gobalParams.windowFullWidth - 110 : e.x - 6) + 'px';
                    // console.log(_this.tabsPopper);
                    e.preventDefault();
                };
                obj.onblur = function () {
                    _this.tabsPopper.isShow = false;
                }
            }
        },
        tabRefresh: function () {
            this.tabActive = this.tabsPopper.id;
            document.getElementById('iframe_' + this.tabsPopper.id).contentWindow.location.reload(true);
        },
        tabClose: function (targetName) {
            api.tabCloseOrCallBack(this.findTab(targetName, this.tabItems));
        },
        tabCloseRight: function () {
            if (this.tabsPopper.id === homeTab.key) {
                this.tabCloseAll();
            } else {
                //this.tabActive = this.tabsPopper.id;
                var tabs = this.tabItems,
                    items = [],
                    size = tabs.length;
                while (--size >= 0) { // 倒叙
                    if (tabs[size].name === this.tabsPopper.id) {
                        break;
                    }
                    items.push(tabs[size]);
                }
                var chain = new SalChain(items, function (tab, index, chain) {
                    api.tabCloseOrCallBack(tab, chain);
                    // chain.next();
                });
                chain.next();
                //this.tabItems = newTabs;
            }
        },
        tabCloseAll: function () {
            var tabs = this.tabItems,
                items = [],
                size = tabs.length;
            while (--size > 0) { // 倒叙
                items.push(tabs[size]);
            }
            var chain = new SalChain(items, function (tab, index, chain) {
                api.tabCloseOrCallBack(tab, chain);
                // chain.next();
            });
            chain.next();
            // this.tabItems = [homeTab];
            // this.tabActive = homeTab.key;
        },
        tabPosition: function () {
            if (this.menuDefaultActive === this.tabsPopper.id) {
                var curr = this.findMenuParent(this.tabsPopper.id, this.menuArr);
                if (curr !== false) {
                    this.$refs.menu.open(curr.key);
                }
            } else {
                this.menuDefaultActive = this.tabsPopper.id;
            }
        },
    },
    mounted: function () {
        //this.$nextTick(function () {
        //    // Code that will run only after the
        //    // entire view has been rendered
        //    //api.jumpTab('useDocTemplate','样板示例','useDocTemplate.html','layui_icon layui_icon_template');
        //});
    }
});
