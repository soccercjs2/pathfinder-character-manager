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

    $('.rollable_button').hammer().on('tap', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = $(this).data("value");
        Roll(roll_label, roll_value);
    });

    $('.rollD20').hammer().on('doubletap', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = MakeD20Roll($(this).data("value"));
        var critical_minimum = 20;
        if ($(this).data('critical-minimum')) { critical_minimum = $(this).data('critical-minimum'); }

        Roll(roll_label, roll_value, critical_minimum);
    });

    $('.rollD20').hammer().on('press', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = MakeD20Roll($(this).data("value"));
        var critical_minimum = 20;
        if ($(this).data('critical-minimum')) { critical_minimum = $(this).data('critical-minimum'); }

        Roll(roll_label, roll_value, critical_minimum);
    });

    $('.rollD20button').hammer().on('tap', function (e) {
        var roll_label = $(this).data("name");
        var roll_value = MakeD20Roll($(this).data("value"));
        var critical_minimum = 20;
        if ($(this).data('critical-minimum')) { critical_minimum = $(this).data('critical-minimum'); }

        Roll(roll_label, roll_value, critical_minimum);
    });
}

function MakeD20Roll(value)
{
    var roll = '1d20 + ' + value;
    
    //if (value >= 0) { roll += '+'; }
    //else { roll += '-' }

    //roll += ' ' + math.abs(parseInt(value));
    return roll;
}

function Roll(label, value, critical_minimum)
{
    var equation = EvaluateRolls(value, critical_minimum);

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

    var attack_status = $('#roll_result').data('attack-status');
    if (attack_status == 'critical') {
        $('#roll_result').addClass('critical');
        $('#roll_result').removeClass('critical-failure');
    }
    else if (attack_status == 'critical-failure')
    {
        $('#roll_result').removeClass('critical');
        $('#roll_result').addClass('critical-failure');
    }
    else if (attack_status == 'normal') {
        $('#roll_result').removeClass('critical');
        $('#roll_result').removeClass('critical-failure');
    }
    else {
        $('#roll_result').removeClass('critical');
        $('#roll_result').removeClass('critical-failure');
    }

    $('#roll_result').fadeToggle("fast", "linear");
    $('#roll_label').fadeIn('fast');
}

function EvaluateRolls(roll_string, critical_minimum)
{
    var dIndex = roll_string.indexOf('d');

    if (dIndex >= 0)
    {
        var diceQuantityStart = FindDiceQuantityStart(roll_string.substring(0, dIndex));
        var diceSizeEnd = FindDiceSizeEnd(roll_string.substring(dIndex + 1)) + dIndex;

        var diceQuantity = roll_string.substring(diceQuantityStart, dIndex);
        var diceSize = parseInt(roll_string.substring(dIndex + 1, diceSizeEnd + 1));
        var roll = '';

        for (i = 0; i < diceQuantity; i++)
        {
            var roll_result = math.randomInt(1, diceSize + 1);
            if (roll != '') { roll += ' + '; }
            roll += '(' + roll_result + ')';

            if (diceSize == 20 && roll_result >= critical_minimum) {
                $('#roll_result').data('attack-status', 'critical');
            }
            else if (diceSize == 20 && roll_result == 1) {
                $('#roll_result').data('attack-status', 'critical-failure');
            }
            else
            {
                $('#roll_result').data('attack-status', 'normal');
            }
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