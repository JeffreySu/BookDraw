# BookDraw：《微信开发深度解析：公众号、小程序高效开发秘籍》1元众筹抽奖程序

抽奖规则：
1. 按照已支付支付1元的次序，对所有抽奖名单进行排序（名单位于[OneYuanCrowdFunding.txt](https://github.com/JeffreySu/BookDraw/blob/master/BookDraw/BookDraw/OneYuanCrowdFunding.txt)下，GB2312编码）
2. 根据2017年7月4日“腾讯控股”股票收盘价格*10并取整作为基数（估价为2843.70，则基数为2837），乘以2017年7月14日20:00时，https://github.com/JeffreySu/WeiXinMPSDK 项目的Fork的数字，作为参数
3. 所有的抽奖人员按照顺序以500人为一组（目前只有一组），组内编号为“参数/500的余数”的人员即为获奖人员。
