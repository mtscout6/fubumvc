(function ($) {
  var exports = {},
        gridSelector,
        filterSelector = '#filter-dialog',
        columnModel = $('#column-model').length ? $('#column-model').metadata({
          type: 'elem', name: 'script'
        }) : undefined,
        filterColumns = function () {
          var cols = [],
              i = 0;
          if (!columnModel) return false;
          
          for (i; i < columnModel.length; i++) {
            var col = columnModel[i];
            if (col.hideFilter) {
              continue;
            }

            cols.push({ Name: col.name });
          }

          return cols;
        },
        setupFilters = function () {
          exports.filterDialog()
                  .dialog({
                    bgiframe: true,
                    autoOpen: false,
                    height: 150,
                    width: 500,
                    modal: true
                  });

          $(document).bind('keydown', 'Ctrl+f', function () {
            exports.filterDialog().dialog('open');
            return false;
          });

          var startupFilters = $('#filters').metadata({ type: 'elem', name: 'script' });
          if (startupFilters.Values) {
            var value = '';
            if (startupFilters.Values.length != 0) {
              value = startupFilters.Values[0];
            }

            viewModel.explicitAddFilter(startupFilters.ColumnName, value);
          }
        },
        setupAutocomplete = function () {
          var filterInput = $('#filter-value'),
              filterUrl = filterInput.metadata().url;

          filterInput.autocomplete({
            source: function (request, response) {
              $.ajax({
                url: filterUrl,
                dataType: 'json',
                type: 'POST',
                data: {
                  Column: viewModel.selectedFilter().Name,
                  Query: request.term
                },
                success: function (data) {
                  response($.map(data.Values, function (item) {
                    return {
                      label: item.ColumnName,
                      value: item.Value
                    };
                  }));
                }
              });
            },
            minLength: 2,
            select: function (event, ui) {
              filterInput.val(ui.item.value);
            }
          });
        },
        getData = function (gridData) {
          var params = {};
          params.page = gridData.page;
          params.rows = gridData.rows;
          params.sidx = gridData.sidx;
          params.sord = gridData.sord;
          params.Filters = viewModel.buildFilters();

          $.ajax({
            type: "POST",
            url: exports.grid().metadata().url,
            data: JSON.stringify(params),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
              exports.grid()[0].addJSONData(data);
            },
            error: function () {
              if (window.console) {
                console.log('An error occurred loading the grid');
              }
            }
          });
        },
        setupGrid = function (gridOptions) {
          var options = {
            datatype: function (gridData) {
              getData(gridData);
            },
            url: exports.grid().metadata().url,
            colNames: exports.colNames(),
            colModel: columnModel,
            jsonReader: $.fubu.jsonReader,
            rowNum: 50,
            autowidth: true,
            height: '100%',
            mtype: 'POST',
            pager: '#pager',
            onCellSelect: function (rowId) {
              window.location = rowId;
            },
            ondblClickRow: function (rowId) {
              window.location = rowId;
            },
            sortable: true
          };

          if (!columnModel.length) {
            $('#NoData').show();
            return;
          }

          exports.grid().jqGrid($.extend(options, gridOptions));

          $(window).bind('resize', function () {
            exports.grid().setGridWidth($('#route-heading').width());
          });
        },
        viewModel = {
          filters: ko.observableArray([]),
          showFilters: function () {
            return this.filters().length != 0;
          },
          selectedFilter: ko.observable(),
          filterValue: ko.observable(),
          findFilter: function (type, value) {
            for (var i = 0; i < this.filters().length; i++) {
              var filter = this.filters()[i];
              if (filter.type == type && filter.value == value) {
                return filter;
              }
            }

            return undefined;
          },
          addFilter: function () {
            var type = this.selectedFilter().Name;
            if (this.findFilter(type, this.filterValue())) {
              return;
            }

            var value = this.filterValue();
            this.explicitAddFilter(type, value);

            this.closeDialog();
            this.reloadGrid();
          },
          explicitAddFilter: function (type, value) {
            var self = this;
            this.filters.push({
              type: type,
              value: value,
              remove: function () {
                self.removeFilter(type, value);
              }
            });
          },
          closeDialog: function () {
            exports.filterDialog().dialog('close');
            this.selectedFilter(undefined);
            this.filterValue('');
          },
          removeFilter: function (type, value) {
            var filter = this.findFilter(type, value);
            if (!filter) {
              return;
            }

            this.filters.remove(filter);
            this.reloadGrid();
          },
          buildFilters: function () {
            var gridFilters = [],
                findFilter = function (colName) {
                  for (var z = 0; z < gridFilters.length; z++) {
                    var f = gridFilters[z];
                    if (f.ColumnName == colName) {
                      return f;
                    }
                  }
                  return undefined;
                };

            for (var i = 0; i < this.filters().length; i++) {
              var filter = this.filters()[i],
                  gridFilter = findFilter(filter.type);
              if (!gridFilter) {
                gridFilter = {
                  ColumnName: filter.type,
                  Values: []
                };
                gridFilters.push(gridFilter);
              }

              gridFilter.Values.push(filter.value);
            }

            return gridFilters;
          },
          reloadGrid: function () {
            exports.grid().jqGrid().trigger('reloadGrid');
          },
          availableFilters: ko.observableArray(filterColumns())
        };

  exports.grid = function () {
    return $(gridSelector);
  };

  exports.filterDialog = function () {
    return $(filterSelector);
  };

  exports.colNames = function () {
    var cols = [];
    for (var i = 0; i < columnModel.length; i++) {
      var col = columnModel[i];
      cols.push(col.name);
    }

    return cols;
  };

  exports.initGrid = function (gridElementSelector, gridOptions) {
    gridSelector = gridElementSelector;
    ko.applyBindings(viewModel);
    setupFilters();
    setupGrid(gridOptions);
    setupAutocomplete();
  };

  $.gridwrapper = exports;

})(jQuery);