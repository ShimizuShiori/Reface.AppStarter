# 0.6.0

## 2019-05-15

* �Դ�������˽ṹ�ϵ��ع�
* �� BaseModuleStarter �� BaseApplicationStarter ������ȥ���� Base ǰ׺
* �������¼����ֵĴ��룬ʹ���û���ֱ��ʹ�� ApplicationStarter �� ModuleStarter ʱ��������ʵ������¼��ķ���
* �Ƴ��� ApplicationStarter �Ͽ���ֱ�Ӽ���Ӧ�ó��������¼����ܣ�����һ�����뵽 Event ������
* Ӧ�ó��򻷾��п��Է������м��ص� IModule ��Ϣ
* Component �����Զ��Է��ͽ���ע�ᣬ��һ���������û���Ҫ�Լ����õĴ�����
* ��ʼд RelaseNotes

# 0.6.1

## 2019-05-01

* �޸��� ReleaseNotes �ļ���

# 0.6.2

## 2019-05-21

* Ϊģ��������µ��¼����ܣ�ÿ��һ�������������ע��ǰ֪ͨ�ⲿ

# 0.6.3

## 2019-05-21

* ��Ӧ�ó��������¼������� autofac �������ڣ��������������¼�����

# 0.7.0

## 2019-05-22

* ��д�ײ��ʵ�֣���д������ģ���Ӧ�õ�API
* ��װ�˼��׵Ķ�Ԫ��ע��ķ������²���ֱ��ʹ�� autofac
* ����˼��׵Ķ�Ԫ����ȡ�ķ������²���ֱ��ʹ�� autofac
* ��չ��һЩģ��ɸ�Ԥ���¼��ڵ�

# 0.8.0

## 2019-05-26

* ������ ComponentAttribute �Ĺ��ܣ�ȡ����Ĭ������Ĺ���
* �Ƴ��� Setup �е� Properties ���ԣ����� Context �����
* �Ƴ��� Environment �е� Properties ���ԣ���Ϊ�� Context
* �����Ż��˴���ṹ

# 0.8.1

## 2019-05-26

* �޸���ע��ʱ���ж��Ƿ���ע��� BUG

# 0.9.0

* rewrite all things

# 0.10.10

* ������µ��¼������ڼ����� Autofac �����������ʱ�����ȴû�б�ע����龰��
* �����¼������� autofac ����׷��Ԫ����ע�ᡣ
* �� autofac ע��Ԫ��ʱ��ֻ��ע��ǳ�����
* �� Func �ķ�ʽ�� AutofacContainerBuilder ע��Ԫ��

# 0.13.0

* �޸��� IAppModule �� OnUsing �ӿڣ���һ�� ģ��A ��Ҫʹ�� ģ��B ʱ���Ὣ ģ��A ��ʵ���ṩ�� ģ��B �� OnUsing �����У��Ӷ������ܵĲ��� AppModule ��ʹ���в����Ĺ��캯��
* �޸��� AppModule ��ʵ�֣�����̳��� Attribute ����ʹ�� AppModule �ϵ� Attribute ���õ���������˶��������ķ���

# 1.0.0

* �����������ע��

# 1.1.0

## 2020-03-25

* ���� *Listener* ���������� **�¼�������** ������
* ������ *CommandBus* ����
* ������ *ComponentCreator** ����
* �� *EventBus* �� *CommandBus* ��ע��ͨ�� *ComponentCreator* ���

# 1.2.0

## 2020-03-26

* ��д�� *AutofacContainerBuilder* �����ע����߼�������ֱ��ע�ᵽ *autofac* �� *builder*�������ȼ�¼���� *AutofacContainerBuilder.Build()* ʱ��ע��
* *AutofacContainerBuilder* �ṩ�� *ServiceType* ɾ��ע��Ĺ��ܣ�������ģ������ע�� *ServiceType* ��ʵ����
* Ϊ *AppSetup* ����¼��������е� *AppModule* �������غ󣬴��� *AllModulesLoaded* �¼�
* ��� *ReplaceCreator* ������������ *AppMOdule* �滻���е����

# 1.5.0

## 2020-03-31

* ��д�� *ReplaceServiceContainerBuilder* ���߼���ͨ������ *AutofacContainerBuilder.Building* �¼�����������滻
* ��д�� *ConfigAppContainerBuilder* ���߼���ͨ������ *AutofacContainerBuilder.Building* �¼�����������滻

# 1.6.0

## 2020-04-07

* **�¹���** ϵͳ����ʱ������ *Json Schema* �ļ��������ļ�����ͨ�����ô� *schema* ʵ����ʾ���ܡ�
* �����½ӿ� *IEmptyAppContainer* ��ʾһ���յ��������� *App* ���Զ�����й�
* �޸� *AppSetup* �е�һЩ�߼�����������֧����ͬһ�� *Library* �д��ڶ�� *AppModule*

# 1.7.0

## 2020-04-10

* ���� *Predicate* ���ܣ�������д�������Ķ��������ж�
* ���� *AutofacContainerBuilder* �Ĺ��ܣ�������ע��������Ƴ���ע�������

# 1.7.1
## 2020-04-012
* ���� *JsonSchema* �ļ��ܹ���Ӧ *Enum* ���ͣ������ܹ�������Ӧ��������Ϣ