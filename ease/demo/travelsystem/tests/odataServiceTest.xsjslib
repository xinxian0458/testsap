/*global jasmine, describe, beforeOnce, beforeEach, it, xit, expect*/
var SqlExecutor = $.import('sap.hana.testtools.unit.util', 'sqlExecutor').SqlExecutor;
var mockstarEnvironment = $.import('sap.hana.testtools.mockstar', 'mockstarEnvironment');
var TableUtils = $.import('sap.hana.testtools.unit.util', 'tableUtils').TableUtils;

describe('Test Cost Assignment', function() {
	var sqlExecutor = null;
	var testEnvironment = null;
	var testTable = 'CostAssignmentTest';

	beforeOnce(function() {
	    var tableUtils = new TableUtils(jasmine.dbConnection);
        tableUtils.createTestTableFromView('ease.demo.travelsystem/COST_ASSIGNMENT', testTable, ['ID'], '_SYS_BIC');
        
		var definition = {
		    //targetPackage : "ease.demo.travelsystem.temp",
			schema : 'TRAVELSYSTEM',
			model : {
				schema : '_SYS_BIC',
				name : 'ease.demo.travelsystem/COST_ASSIGNMENT' //e.g. package/MODEL
			},
			substituteTables : {
			    "table" : { 
					name : 'CostAssignment',
					testTable: '"TRAVELSYSTEM"."CostAssignmentTest"'
				}
			}
		};
		testEnvironment = mockstarEnvironment.defineAndCreate(definition);
	});
	
	/*afterOnce(function() {
		sqlExecutor = new SqlExecutor(jasmine.dbConnection);
		sqlExecutor.execSingle('Drop view ' + testEnvironment.getTestModelName());
		sqlExecutor.execSingle('Drop table "' + testTable + '"');
	});*/

	beforeEach(function() {
		sqlExecutor = new SqlExecutor(jasmine.dbConnection);
		testEnvironment.clearAllTestTables();
	});

	it('Test Cost Assignment creation', function() {
		var data = {
		  ID : 1,
		  Desc : "Cost Assignment Description"
		};
		var expectedData = {
			ID : [ 1 ],
			Desc : [ "Cost Assignment Description" ]
		};
		testEnvironment.fillTestTable("table", data);

        //var actualData = sqlExecutor.execQuery('select * from "_SYS_BIC"."tmp.unittest.travelsystem.ease.demo.travelsystem/COST_ASSIGNMENT"');
		var actualData = sqlExecutor.execQuery('select * from ' + testEnvironment.getTestModelName());
		expect(actualData).toMatchData(expectedData, [ "ID" ]);

	});
});


