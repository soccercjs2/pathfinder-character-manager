﻿//create hammer event listener
window.onload = function () {
    (function (factory) {
        if (typeof define === 'function' && define.amd) {
            define(['jquery', 'hammerjs'], factory);
        } else if (typeof exports === 'object') {
            factory(require('jquery'), require('hammerjs'));
        } else {
            factory(jQuery, Hammer);
        }
    }(function ($, Hammer) {
        function hammerify(el, options) {
            var $el = $(el);
            if (!$el.data("hammer")) {
                $el.data("hammer", new Hammer($el[0], options));
            }
        }

        $.fn.hammer = function (options) {
            return this.each(function () {
                hammerify(this, options);
            });
        };

        // extend the emit method to also trigger jQuery events
        Hammer.Manager.prototype.emit = (function (originalEmit) {
            return function (type, data) {
                originalEmit.call(this, type, data);
                $(this.element).trigger({
                    type: type,
                    gesture: data
                });
            };
        })(Hammer.Manager.prototype.emit);
    }));

    ListenForSingleD20Roll();
    ListenForAttackRoller();
}

function ListenForSingleD20Roll() {
    $('.single_roll').on('mousedown', function (e) {
        $(this).addClass('single_roll_selected');
    });

    $('.single_roll').hammer().on('press', function (e) {
        SingleD20Roll($(this).data('name'), $(this).data('value'));
        $(this).removeClass('single_roll_selected');
    });

    $('.single_roll').hammer().on('doubletap', function (e) {
        SingleD20Roll($(this).data('name'), $(this).data('value'));
    });

    $('.single_roll').on('mouseup', function (e) {
        $(this).removeClass('single_roll_selected');
    });

    $('#btnRoll').click(function (e) {
        var value = $('#btnRoll').data('value')

        $('#rollResult').fadeOut(200, function () { UpdateRoller($('#rollResult'), $('#singleD20RollString'), Roll('1d20'), value); });
        $('#rollResult').fadeIn(200);
    });
}

function ListenForAttackRoller() {
    $('.attack_roll').hammer().on('press', function (e) {
        AttackRoller('Roll Attacks', $(this).data('weapon'), $(this).data('bonuses'), $(this).data('damage'), $(this).data('critical'));
    });

    $('.attack_roll').hammer().on('doubletap', function (e) {
        AttackRoller('Roll Attacks', $(this).data('weapon'), $(this).data('bonuses'), $(this).data('damage'), $(this).data('critical'));
    });

    $('#tblAttacks').on('click', 'button', function () {
        if ($('#txtAttackBonus').val() != '') { var attackBonus = $('#txtAttackBonus').val(); } else { attackBonus = 0; }
        if ($('#txtDamageBonus').val() != '') { var damageBonus = $('#txtDamageBonus').val(); } else { damageBonus = 0; }
        if ($('#txtCriticalMultiplier').val() != '') { var criticalMultiplier = $('#txtCriticalMultiplier').val(); } else { criticalMultiplier = 1; }

        var equation = $(this).data('equation');
        var rollString = '';
        var splitEquation = equation.split(' ');
        var isCritical = false;
        var isCriticalFailure = false;
        var criticalMin = $(this).data('critical');

        for (i = 0; i < splitEquation.length; i++) {
            if (rollString != '') { rollString += ' '; }
            if (splitEquation[i].indexOf('d') > -1) {
                var roll = Roll(splitEquation[i]);
                if (splitEquation[i] == "1d20" && roll >= criticalMin) { isCritical = true; }
                if (splitEquation[i] == "1d20" && roll == 1) { isCriticalFailure = true; }
                rollString += '(' + roll + ')';
            } else {
                rollString += splitEquation[i];
            }
        }

        if ($(this).data('type') == 'attack' && attackBonus != 0) {
            equation += ' + ' + attackBonus;
            rollString += ' + ' + attackBonus;
        } else if ($(this).data('type') == 'damage') {
            if (damageBonus != 0) {
                equation += ' + ' + damageBonus;
                rollString += ' + ' + damageBonus;
            }
            if (criticalMultiplier != 1) {
                equation = criticalMultiplier + ' * (' + equation + ')';
                rollString = criticalMultiplier + ' * (' + rollString + ')';
            }
        }

        var result = math.eval(rollString);
        $(this).fadeOut(200, function () {
            if (isCritical) {
                $(this).addClass("btn-success");
            } else if (isCriticalFailure) {
                $(this).addClass("btn-danger");
            } else {
                $(this).removeClass("btn-success");
                $(this).removeClass("btn-danger");
            }

            $('#attackRollString').text(equation + ' = ' + rollString + ' = ' + result);
            $(this).text(result);
        });
        $(this).fadeIn(200);
    });
}