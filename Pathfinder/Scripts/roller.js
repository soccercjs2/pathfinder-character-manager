function RollbarListener()
{
    $('#roll_result').hammer().on('doubletap', function (e) {
        $(this).fadeToggle("fast", function () {
            $('#roller').fadeToggle("fast", "linear");
        });
    });

    $('#roll_result').hammer().on('press', function (e) {
        $(this).fadeToggle("fast", function () {
            $('#roller').fadeToggle("fast", "linear");
        });
    });

    $('#roll_result').hammer().on('tap', function (e) {
        $('#roll_value').fadeToggle("fast", function () {
            var label = $('#roll_label').data('label');
            var value = $('#roll_value').data('value');
            var equation = $('#roll_value').data('equation');

            if ($('#roll_value').text() == value) {
                $('#roll_value').text(equation);
                $('#roll_label').fadeOut('fast');
            }
            else if ($('#roll_value').text() == equation) {
                $('#roll_value').text(value);
                $('#roll_label').fadeIn('fast');
            }

            $('#roll_value').fadeToggle("fast", "linear");
        });
    });

    $('#roll_button').click(function () {
        var roll_string = $('#roll_input').val();
        Roll(roll_string, roll_string);
    });

    $('.rollable').hammer().on('doubletap', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = $(this).data("value");
        Roll(roll_label, roll_value);
    });

    $('.rollable').hammer().on('press', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = $(this).data("value");
        Roll(roll_label, roll_value);
    });

    $('.rollD20').hammer().on('doubletap', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = '1d20 + ' + $(this).data("value");
        Roll(roll_label, roll_value);
    });

    $('.rollD20').hammer().on('press', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = '1d20 + ' + $(this).data("value");
        Roll(roll_label, roll_value);
    });
}

function Roll(label, value)
{
    var equation = EvaluateRolls(value);

    if ($('#roller').css('display') == 'none')
    {
        $('#roll_result').fadeToggle("fast", function () {
            ShowResult(label, equation);
        });
    }
    else
    {
        $('#roller').fadeToggle("fast", function () {
            ShowResult(label, equation);
        });
    }
}

function ShowResult(label, equation)
{
    var value = math.eval(equation);
    $('#roll_label').text(label);
    $('#roll_label').data('label', label);
    $('#roll_value').data('value', value);
    $('#roll_value').data('equation', equation);
    $('#roll_value').text(value);
    $('#roll_result').fadeToggle("fast", "linear");
}

function EvaluateRolls(roll_string)
{
    var dIndex = roll_string.indexOf('d');

    if (dIndex >= 0)
    {
        var diceQuantityStart = FindDiceQuantityStart(roll_string.substring(0, dIndex));
        var diceSizeEnd = FindDiceSizeEnd(roll_string.substring(dIndex + 1)) + dIndex;

        var diceQuantity = roll_string.substring(diceQuantityStart, dIndex);
        var diceSize = roll_string.substring(dIndex + 1, diceSizeEnd + 1);
        var roll = '';

        for (i = 0; i < diceQuantity; i++)
        {
            if (roll != '') { roll += ' + ';}
            roll += '(' + math.randomInt(1, diceSize) + ')';
        }

        if (diceSizeEnd + 1 == roll_string.length) {
            return roll_string.substring(0, diceQuantityStart) + roll;
        }
        else
        {
            return roll_string.substring(0, diceQuantityStart) + roll + EvaluateRolls(roll_string.substring(diceSizeEnd + 1));
        }
    }
    else
    {
        return roll_string;
    }

}

function FindDiceQuantityStart(prefix)
{
    var index = prefix.length - 1;
    while (index - 1 > 0 && IsNumeric(prefix[index - 1])) { index--; }
    return index;
}

function FindDiceSizeEnd(suffix) {
    var index = 0;
    while (index < suffix.length && IsNumeric(suffix[index])) { index++; }
    return index;
}

function IsNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
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

//create hammer event listener
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

    //ListenForSingleD20Roll();
    //ListenForAttackRoller();
    RollbarListener();
}