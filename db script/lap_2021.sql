/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 80025
 Source Host           : 127.0.0.1:3306
 Source Schema         : lap_2021

 Target Server Type    : MySQL
 Target Server Version : 80025
 File Encoding         : 65001

 Date: 05/08/2021 17:00:59
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for logs
-- ----------------------------
DROP TABLE IF EXISTS `logs`;
CREATE TABLE `logs`  (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '主键',
  `module_code` int NOT NULL COMMENT '项目模块代码',
  `level` int NOT NULL COMMENT '日志等级,1-调试信息(Debug),2-普通信息(Info),3-警告信息(Warn),4-错误信息(Error),5-非常严重的错误(Fatal)',
  `request_path` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '请求路径',
  `request_url` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '请求地址',
  `request_form` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '请求内容',
  `method` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求方式(get、post、put、delete...)',
  `exception` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '错误信息',
  `message` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '信息',
  `ip_address` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'ip地址',
  `remark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `log_create_time` datetime(0) NOT NULL COMMENT '日志创建时间',
  `created_time` datetime(0) NOT NULL COMMENT '创建实际',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2301 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '错误日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for modules
-- ----------------------------
DROP TABLE IF EXISTS `modules`;
CREATE TABLE `modules`  (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '主键',
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '模块名称',
  `code` int NOT NULL COMMENT '模块代码',
  `created_by` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `created_time` datetime(0) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '项目模块表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for statistic_logs
-- ----------------------------
DROP TABLE IF EXISTS `statistic_logs`;
CREATE TABLE `statistic_logs`  (
  `id` int NOT NULL AUTO_INCREMENT COMMENT '主键',
  `module_code` int NOT NULL COMMENT '项目模块代码',
  `request_page` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '访问页面',
  `action` int NOT NULL COMMENT '执行动作,例如：登录、登出...',
  `request_url` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '访问地址',
  `message` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '信息说明',
  `request_time` datetime(0) NOT NULL COMMENT '访问时间',
  `created_time` datetime(0) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 751 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '统计日志表' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
