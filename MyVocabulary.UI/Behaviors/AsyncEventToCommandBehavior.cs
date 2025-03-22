using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MyVocabulary.UI.Behaviors;

/// <summary>
/// A behavior that allows binding asynchronous commands to events.
/// Unlike <see cref="EventToCommandBehavior"/>, it supports <see cref="IAsyncRelayCommand"/>.
/// </summary>
public class AsyncEventToCommandBehavior : Behavior<VisualElement>
{
    
    public static readonly BindableProperty EventNameProperty =
        BindableProperty.Create(nameof(EventName), typeof(string), typeof(AsyncEventToCommandBehavior));

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AsyncEventToCommandBehavior));

    public string EventName
    {
        get => (string)GetValue(EventNameProperty);
        set => SetValue(EventNameProperty, value);
    }

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private VisualElement AssociatedObject { get; set; } = null!;

    protected override void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        AssociatedObject = bindable;

        bindable.BindingContextChanged += OnBindingContextChanged;
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        UnregisterEvent(bindable);
        bindable.BindingContextChanged -= OnBindingContextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnBindingContextChanged(object? sender, EventArgs e)
    {
        BindingContext = AssociatedObject.BindingContext;
        RegisterEvent();
    }

    private void RegisterEvent()
    {
        if (Command is null)
            return;
        
        var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(EventName);
        if (eventInfo == null) return;

        var methodInfo = GetType().GetMethod(nameof(OnEventTriggered),
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        if (methodInfo == null) return;

        var handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType!, this);
        eventInfo.AddEventHandler(AssociatedObject, handler);
    }

    private void UnregisterEvent(VisualElement bindable)
    {
        var eventInfo = bindable.GetType().GetRuntimeEvent(EventName);
        if (eventInfo == null) return;

        var methodInfo = GetType().GetRuntimeMethod(nameof(OnEventTriggered), [typeof(object), typeof(EventArgs)]);
        if (methodInfo == null) return;

        var handler = methodInfo.CreateDelegate(eventInfo.EventHandlerType!, this);
        eventInfo.RemoveEventHandler(bindable, handler);
    }

    private async void OnEventTriggered(object sender, EventArgs e)
    {
        if (Command is IAsyncRelayCommand asyncCommand && asyncCommand.CanExecute(null))
            await asyncCommand.ExecuteAsync(null);
        else if (Command!.CanExecute(null))
            Command.Execute(null);
    }
}