import { Modal, Form, Input, Button, message } from 'antd';
import { useRef } from 'react';
import { insertSystemForm } from '@/services/ant-design-pro/form'
import { ColumnType } from '@/services/ant-design-pro/typings.d.ts'
import AttributeText from './attribute/text'
import AttributeTextarea from './attribute/textarea'
import AttributeNumber from './attribute/number'
import AttributeDate from './attribute/date'
import AttributeDaterange from './attribute/daterange'
import AttributeEditor from './attribute/editor'
import AttributeImage from './attribute/image'
import AttributeSelect from './attribute/select'
import AttributeStatic from './attribute/static'
// eslint-disable-next-line @typescript-eslint/ban-types
import AttributeVideo from './attribute/video'
const AttributeModel = (props: { modalVisible: boolean, hiddenModal: Function, refresh: Function, currentRecord: any }) => {
    const { modalVisible, hiddenModal, refresh, currentRecord } = props
    console.log(props.modalVisible, modalVisible, 'ssss----ss')

    console.log(currentRecord, 'currentRecord')


    const handleOk = () => {
        hiddenModal()
    }
    const columnType = ColumnType[currentRecord.type]

    const getAttributeComponent = (
        // type: ColumnType,
        // props: { attribute: any; cId: string },
    ) => {
        const type = currentRecord.type
        const attributeComponent: { [T in ColumnType]: React.ReactElement } = {
            date: <AttributeDate />,
            daterange: <AttributeDaterange />,
            editor: <AttributeEditor />,
            image: <AttributeImage />,
            number: <AttributeNumber />,
            select: <AttributeSelect />,
            static: <AttributeStatic />,
            text: <AttributeText />,
            textarea: <AttributeTextarea />,
            video: <AttributeVideo />
        };
        return attributeComponent[type];
    };

    const formRef = useRef(null);
    const [form] = Form.useForm();

    const handleCancel = () => {
        form.resetFields()
        hiddenModal()
    }

    const onSubmit = async (values: any) => {
        const result = await insertSystemForm({
            formName: values.formName,
        })
        if (result.code === 200) {
            message.success('新增表单成功')
            handleCancel()
            refresh()
            // TODO 还没有刷新左侧树
        }
        console.log(result, '保存结果')
    }

    return (
        <Modal title={currentRecord.title + '-' + columnType} footer={null} visible={modalVisible} onOk={handleOk} onCancel={handleCancel}>
            <Form form={form} onFinish={(values: any) => onSubmit(values)} ref={formRef}>
                {/* <Form.Item
          label="表单名称"
          name="formName"
          rules={[{ required: true, message: '请输入表单名称' }]}
        >
          <Input />
        </Form.Item> */}
                {getAttributeComponent()}
                <Form.Item style={{ textAlign: 'right' }}>
                    <Button type="primary" htmlType="submit" style={{ marginRight: '20px' }} >
                        保存
                    </Button>
                    <Button htmlType="button" onClick={handleCancel}>
                        取消
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default AttributeModel

