'use strict';

var WriteToBus = require('../knx_eibd').WriteToBus;

exports.dim = function(req, res){
	if (req.body.data) {
		var data = req.body.data;
		WriteToBus(data.Address, data.DPT, data.Value);

		res.end('finished');		
	}
	res.end('finished');	
}

exports.switch = function (req, res) {
	if (req.body.data) {
		var data = req.body.data;
		WriteToBus(data.Address, data.DPT, data.Value);

		res.end('finished');
	}
	res.end('finished');
}
